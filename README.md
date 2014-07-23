Fiddler-SharePoint-Designer-Settings
====================================

With this Fiddler extension you can activate SharePoint Designer access to a SharePoint site, even if it has been disabled by the administrator.
If you start SharePoint Designer, the server send a flag wether SharePoint Designer is allowed or not. This Fiddler extension rewrites this and some other flags, to "re-enable" SharePoint Designer.
Please keep in mind that you still need the necessary permissions to make any changes.

I've tested the "hack" on SharePoint 2010 and 2013.


To install the fiddler extension do the following:
Copy `FiddlerSPDSettings.dll` to `\My Documents\Fiddler2\Scripts` to make the extension available to the current user, or
copy `FiddlerSPDSettings.dll` to `\Program Files\Fiddler2\Scripts` to make the extension available to all users on the machine
Restart fiddler after copying the file.
