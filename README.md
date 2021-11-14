# Our.HiddenInput
An additional property type for Umbraco!
The **Our.HiddenInput** property type allows an developer to set constants for content types in the backoffice without the fear of beeing changed by the editor.
The properties based on this types will not be shown within in the Content section, editing is only possible in the Settings section.
This package is perfect for you if you have constants within your code that could change at some point but should never be changed by the editor but only by the developer!

## Usage
When creating or changing a HiddenInput field you are able to insert a Value as string. This value can only be changed here!
If you are updating the value every content that has the property type on it will be updated to have the updated value.

In code the property type will return a simple string containing the entered value.
