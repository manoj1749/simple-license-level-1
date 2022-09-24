# Simple License Level-1 Walkthrough

This user will be given the appgui binary which is a .Net client which can be used to communicate to a server.
The client when launched will ask the user to enter a license key. The client enters the license key and if the entered license key is correct we will get the flag or else we will be asked for a valid license key.

So the first order of business is to figure out the correct license file or circumvent the licensing mechanism.

# Cracking The License Key

We are presented with a dotnet binary. If we load it up in dotpeek, we can see that it converts the license key from the user into a Base64 string and compares if it is equal to "MTIzNDcyMzA5NTcyMzkwNTM=", and sends a message if it is true, where it recives the flag in return.
So by using the Base64 decoder we will be converting the string which it is being compared from which we will get the license key, which is "12347230957239053".
Then by entering the obtained license key and entering in the .Net client will provide us the flag
