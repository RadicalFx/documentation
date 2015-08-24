###Frictionless: convention based

Our first aim is to remove friction, it is not always easy and cannot be done every single time, but one thing that can give a lot of benefits in this area is to move from a configuration based toolkit to a convention based toolkit, we suppose that this concept is widely accepted and is nothing new.

What happens when these lines of code are executed:

```c#
public partial class App : Application
{
    public App()
    {
        var bootstrapper = new WindsorApplicationBootstrapper<MainView>();
    }
}
```

A lot of things:

1. The application Startup event is wired;
2. When the Startup event is fired:
  1. The Inversion of Control container is created;
  2. The MEF composition container is created;
  3. The composition container is composed against the bootstrapper itself;
  4. The Inversion of Control container is configured using the **[[bootstrap conventions]]**;
  5. The main window (the one identified by the TShellView generic parameter) is resolved and shown;