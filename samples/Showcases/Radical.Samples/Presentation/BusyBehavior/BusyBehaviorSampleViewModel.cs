using System;
using System.Threading;
using System.Threading.Tasks;
using Radical.ComponentModel;
using Radical.Samples.ComponentModel;

namespace Radical.Samples.Presentation.BusyBehavior
{
	[Sample( Title = "BusyStatus behavior", Category = Categories.Behaviors )]
	class BusyBehaviorSampleViewModel : SampleViewModel
	{
		readonly IDispatcher dispatcher;

		public BusyBehaviorSampleViewModel( IDispatcher dispatcher )
		{
			this.dispatcher = dispatcher;
		}

		public bool ThresholdElapsed
		{
			get { return GetPropertyValue( () => ThresholdElapsed ); }
			private set { SetPropertyValue( () => ThresholdElapsed, value ); }
		}

		public bool IsBusy
		{
			get { return GetPropertyValue( () => IsBusy ); }
			private set { SetPropertyValue( () => IsBusy, value ); }
		}

		public string Status
		{
			get { return GetPropertyValue( () => Status ); }
			private set { SetPropertyValue( () => Status, value ); }
		}

		Worker w = null;

		public void CancelWork()
		{
			if ( w != null )
			{
				lock ( this )
				{
					if ( w != null )
					{
						w.CancelWork();
					}
				}
			}
		}

		public async void WorkAsync()
		{
			IsBusy = true;
			Status = "running...";

			w = new Worker()
			{
				OnThresholdElapsed = () => dispatcher.Dispatch( () => ThresholdElapsed = true )
			};

			var r = await w.Execute( token =>
			{
				var count = 0;
				while ( count < 15 && !token.IsCancellationRequested )
				{
					++count;
					Thread.Sleep( 1000 );
				}
			} );

			lock ( this )
			{
				w = null;
			}

			Status = r.Cancelled
				? "cancelled."
				: "completed.";

			IsBusy = false;
		}
	}

	class Worker
	{
		public class Result
		{
			public bool Cancelled { get; set; }
		}

		CancellationTokenSource cs = null;

		public Worker()
		{
			OnThresholdElapsed = () => { };
		}

		public Action OnThresholdElapsed { get; set; }

		public async Task<Result> Execute( Action<CancellationToken> action )
		{
			cs = new CancellationTokenSource();
			var token = cs.Token;

			var r = await Task.Factory.StartNew( () =>
			{
				var threshold = new System.Timers.Timer( 5000 );
				threshold.AutoReset = false;
				threshold.Elapsed += ( s, e ) => OnThresholdElapsed();
				threshold.Start();

				action( token );

				threshold.Stop();

				return new Result() { Cancelled = token.IsCancellationRequested };
			}, cs.Token );

			lock ( this )
			{
				cs = null;
			}

			return r;
		}

		public void CancelWork()
		{
			if ( cs != null )
			{
				lock ( this )
				{
					if ( cs != null )
					{
						cs.Cancel();
					}
				}
			}
		}
	}
}
