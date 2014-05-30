ServiceManager
==============

Just a little tool to make turning selected services on or off

I wrote this because I do all my development on a Surface Pro 2 and I figured out that if I turn off all the services that run to power the application developed by my company, I could extend the battery life by as much as 50%. Given I don't develop that application 24/7, I wanted a basic application to display the status of selected services and to allow me to turn one or all of them on or off with a single button click.

If you want to use it, configuration data is stored in the registry at:
HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Solmundr\ServiceManager

Configuration entries are basically string values with the key being the service name and the value being a friendly label

Updates:

30/05/2014 : Added the ability to maintain the services being used by the application.

TO DO:

Shift Configuration data out of the Registry