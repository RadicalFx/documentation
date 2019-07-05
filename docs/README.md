## MVVM and UI Composition quick start

### Windows Presentation Foundation

#### Steps to bootstrap your project in 3 minutes

* Create a new Visual Studio solution with a WPF application project;
* Add, using nuget, a reference to: [Radical.Windows.Presentation.CastleWindsor](https://www.nuget.org/packages/Radical.Windows.Presentation.CastleWindsor);
  * this will give us the full Radical Presentation stack with the default Castle Windsor support as IoC/DI container;
* Delete the default MainWindow.xaml;
* Edit the app.xaml file to remove the StartupUri attribute;
* Add a “Presentation” folder to the project;
  * Presentation is the default location, based on a convention, where Radical Presentation looks for views and view models;
* Create 2 new items:
  * A WPF window named Main**View**.xaml \(\*View is important for the default conventions\);
  * A class Main**ViewModel** \(&lt;_ViewName_&gt;ViewModel is important for the default conventions\);
* In the app.xaml.cs add a single line of code:

```csharp
public partial class App : Application
{
   public App()
   {
      var bootstrapper = new WindsorApplicationBootstrapper<Presentation.MainView>();
   }
}
```

**Press F5 and you are up & running**: the MainView window will be shown.

The application boots, all the default and required services \(for MVVM and UI Composition\) are wired into Castle Windsor, the MainView is designed as the main window, at boot time the MainView is resolved and using the conventions engine the MainViewModel is setup and set as the DataContext of the MainView, in the end the MainView is shown.

#### What’s next

The best topic to read now is basic [concepts about the ViewModel](mvvm/abstract-view-model.md).

## Release management process

Radical follows a set of rules to prepare and publish releases:

* Define the milestone;
* Define an issue for everything that gets touched:
  * the initial issue comment must be as descriptive as possibile;
  * the first 30 lines of the first issue comment will be included by default in the release description;
  * to include a different amount of lines add a HR \(--\) to the issue first comment;
  * the issue must be labeled at least with one, and only one, of the following labels: Bug, Feature, Improvement;
* Associate the issue to the milestone;
* Use [GitHub Flow](http://scottchacon.com/2011/08/31/github-flow.html) to commit changes;
* Associate a commit with an issue and close it;
* Publish the release associated to the milestone;

This routines allows us to be able to auto-generate [release notes](https://github.com/RadicalFx/radical/blob/develop/ReleaseNotes.md), trying to be compatible with the [Semantic Release Notes](http://www.semanticreleasenotes.org/) using a [release notes compiler](https://github.com/Particular/GitHubReleaseNotes).

## Contribution guideline

Your contributions to Radical are very welcome.  
If you find a bug, please raise it as an issue.  
Even better fix it and send a pull request.  
If you like to help out with existing bugs and feature requests just check out the list of [issues](https://github.com/RadicalFx/radical/issues) and grab and fix one:

* If you find a bug, please raise it as an issue, even better followed by a pull request.
* If you like to help out with existing bug and feature, just check out the list of [issues](https://github.com/RadicalFx/radical/issues) and grab and fix one.
* This project uses [GitHub flow](http://scottchacon.com/2011/08/31/github-flow.html) for pull requests. So if you want to contribute, fork the repo, create a descriptively named branch off of master \(ie: portable-class-library-support\), fix an issue, run all the unit tests, and send a PR if all is green.
* Please rebase your code on top of the latest commits. Before working on your fork make sure you pull the latest so you work on top of the latests commits to avoid merge conflicts. Also before sending the PR please rebase your code as there is a chance there have been new commits pushed after you pulled last.
* We will only merge PR that could be automatically merged.

## A note on versioning

Radical follows the following versioning scheme:

major.minor.patch-extensions.version

We use the following [semantic versioning policy](http://semver.org/):

```
major           - version when you make incompatible API changes.
minor           - when you add functionality in a backwards-compatible manner.
patch           - when you make backwards-compatible bug fixes.
extensions      - pre-release extensions
version         - pre-release version
```

Check the [Release pages](https://github.com/RadicalFx/radical/releases) for the version history of all the Radical's packages.

## Samples

The Radical source code includes several samples that are divided per scope and technology, samples are available in the documentation repository: [https://github.com/RadicalFx/documentation/tree/master/samples](https://github.com/RadicalFx/documentation/tree/master/samples)

All samples are constantly under heavy development and are also used to test Radical features.

## MyGet unstable feed

Radical uses MyGet to publish unstable releases during development, to use the unstable feed:

* create a `nuget.config` file in the same folder as your solution folder
* add the following content to the configuration file:

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageRestore>
    <clear />
    <add key="enabled" value="True" />
    <add key="automatic" value="True" />
  </packageRestore>
  <packageSources>
    <add key="nuget.org" value="https://www.nuget.org/api/v2/" />
    <add key="Radical Unstable" value="https://www.myget.org/F/radical-unstable/api/v2" />
  </packageSources>
  <disabledPackageSources />
  <config>
    <add key="DependencyVersion" value="HighestMinor" />
  </config>
  <activePackageSource>
    <clear />
    <add key="All" value="(Aggregate source)" />
  </activePackageSource>
</configuration>
```

* close and reopen the solution

By going to the Manage Nuget Packages page of your solution, you'll now see a "Radical Unstable" option in the source selection dropdown. Do not forget to check the "prerelease versions" checkbox search setting.
