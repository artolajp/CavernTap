# Introduction
Itavio is an allowance management solution that gives parents peace of mind that their children are playing games that respect parental demands. Through integration of Itavio, developers and publishers can easily increase their opportunities for conversion by accessing the pre-committed revenue allocated py the parents. Rebates due to accidental overspend and negative revues affect the bottom line; Itavio is here to help.

The Itavio SDK is designed to be as easy as possible to integrate with a basic setup completed in minutes.

# Integration Instructions
#### Unity
The Itavio Unity plugin requires **Unity 5.0.1 or greater**.
#### Android
The ItavioSdk requires a **minimum Android API level 16**.
#### iOS
The ItavioSdk requires **iOS Sdk 8 or greater**.
Up to date Xcode is recommended.

To integrate ItavioSdk into your Unity application:

## Step 1 - Import the UnityPackage
1.  From the menu _**Assets > Import Package > Custom Package...**_
2.  Select the `ItavioUnityPlugin.unitypackage`, _**Open**_ then _**Import**_.

## Step 2 - Define Platforms
1.  Select `/Assets/Plugins/Itavio/Resources/itavioConfig`
2.  In the **Inspector**, add a new platform, name it and fill out the values for the desired environment with the Itavio provided *secret key id* and *secret key*.

## Step 3 - Initialize Itavio
1.  Add this directive to the `MonoBehaviour` that will be making calls to the `ItavioManager`:
    ```csharp
    using itavio;
    ```
2.  In the `Start` method call `itavioManager.initialize(PLATFORM_NAME)` with the name of the target platform defined in the **itavioConfig** file.
    ```csharp
    void Start()
    {
        itavioManager.initialize(PLATFORM_NAME);
    }
    ```

## Step 4 - Using Itavio
Be sure that `itavioManager` has been initialized before calling any of these methods.

### Directives
1.  Use the `itavio` directive
    ```csharp
    using itavio;
    ```

### Linking to the Itavio Parent App _(iOS Only)_
1.  To link with the Parent App use
    ```csharp
    #if UNITY_IOS
    itavioManager.linkWithParentApp(SHOW_GET_APP_DIALOG);
    #endif
    ```
    To re-link with the Parent App use
    ```csharp
    #if UNITY_IOS
    itavioManager.linkWithParentApp(SHOW_GET_APP_DIALOG, true);
    #endif
    ```
    if `SHOW_GET_APP_DIALOG` is `true` the user will be prompted to download the Parent App if it is not installed
2.  The plugin will respond by firing the `OnLinkWithParentApp` event
    ```csharp
    void Start()
    {
      //...
      #if UNITY_IOS
      itavioManager.OnLinkWithParentApp += itavioManager_OnLinkWithParentApp;
      #endif
    }

    #if UNITY_IOS
    void itavioManager_OnLinkWithParentApp(bool result)
    {
        linkedWithParentApp = result; // True if successfully linked to the ParentApp
    }
    #endif
    ```
3.  To check the link status use the property `itavioManager.IsLinked`
    ```csharp
    if(itavioManager.IsLinked) {
      // If Itavio has already been linked against an account
    }
    ```

### Performing a purchase
1.  To approve and start a purchase use:
    ```csharp
    itavioManager.startDebit<PURCHASE_DELEGATE_TYPE>(AMOUNT, CURRENCY_CODE, PURCHASE_DELEGATE, PURCHASE_DELEGATE_ARGUMENTS);
    ```
    The `PURCHASE_DELEGATE` will be called if the account has enough of funds remaining in their balance.
2.  Once the purchase has completed from the store inform Itavio that the purchase has been completed
    ```csharp
    itavioManager.finalizeDebit(true);
    ```
    Alternatively, if the purchase was cancelled or failed
    ```csharp
    itavioManager.finalizeDebit(false);
    ```

### Check for the Parent Itavio App
1.  To determine whether or not the parent app is installed on a user's device use
    ```csharp
    itavioManager.checkForParent(SHOW_GET_APP_DIALOG);
    ```
    Passing `true` will prompt the user to get the app if it is not install.
2.  The plugin will respond by firing the `OnCheckForParent` event
    ```csharp
    void Start()
    {
      //...
      itavioManager.OnCheckForParent += itavioManager_OnCheckForParent;
    }

    void itavioManager_OnCheckForParent(bool result)
    {
        hasParentApp = result; // True if ParentApp is installed, otherwise false
    }
    ```

### Check the user's remaining balance
1.  To get the user's remaining balance use
    ```csharp
    itavioManager.getBalance();
    ```
2.  The plugin will respond by firing the `OnGetBalance` event
    ```csharp
    void Start()
    {
      //...
      itavioManager.OnGetBalance += itavioManager_OnGetBalance;
    }

    void itavioManager_OnGetBalance(double result)
    {
        balance = result; // User's remaining balance
    }
    ```

### Additional events
1.  There are some addition events worth noting
    ```csharp
    void Start()
    {
      //...
      itavioManager.OnStartDebit += itavioManager_OnStartDebit;
      itavioManager.OnCancelDebit += itavioManager_OnCancelDebit;
      itavioManager.OnCompleteDebit += itavioManager_OnCompleteDebit;
      itavioManager.OnError += itavioManager_OnError;
    }

    void itavioManager_OnStartDebit(bool result)
    {
        if (result)
        {
            // Debit has started ~ this is when the purchase delegate is called
        }
    }

    void itavioManager_OnCancelDebit(bool result)
    {
        if (result)
        {
            // Debit was cancelled
        }
    }

    void itavioManager_OnCompleteDebit(bool result)
    {
        if (result)
        {
            // Debit completed
        }
    }

    void itavioManager_OnError(int code, string message)
    {
        // An error has occurred
    }
    ```

# Additional Build Steps
#### iOS
The `itavio.framework` includes support for the iOS Simulator. The AppStore will reject an app with those architectures included, the following step will strip out any unused architectures from all the embedded frameworks.
1. In **XCode** add a new *Run Script* build phase. The shell should be `/bin/sh` and the script should be:
    ```sh
    APP_PATH="${TARGET_BUILD_DIR}/${WRAPPER_NAME}"

    # This script loops through the frameworks embedded in the application and
    # removes unused architectures.
    find "$APP_PATH" -name '*.framework' -type d | while read -r FRAMEWORK
    do
        FRAMEWORK_EXECUTABLE_NAME=$(defaults read "$FRAMEWORK/Info.plist" CFBundleExecutable)
        FRAMEWORK_EXECUTABLE_PATH="$FRAMEWORK/$FRAMEWORK_EXECUTABLE_NAME"
        echo "Executable is $FRAMEWORK_EXECUTABLE_PATH"

        EXTRACTED_ARCHS=()

        for ARCH in $ARCHS
        do
            echo "Extracting $ARCH from $FRAMEWORK_EXECUTABLE_NAME"
            lipo -extract "$ARCH" "$FRAMEWORK_EXECUTABLE_PATH" -o "$FRAMEWORK_EXECUTABLE_PATH-$ARCH"
            EXTRACTED_ARCHS+=("$FRAMEWORK_EXECUTABLE_PATH-$ARCH")
        done

        echo "Merging extracted architectures: ${ARCHS}"
        lipo -o "$FRAMEWORK_EXECUTABLE_PATH-merged" -create "${EXTRACTED_ARCHS[@]}"
        rm "${EXTRACTED_ARCHS[@]}"

        echo "Replacing original executable with thinned version"
        rm "$FRAMEWORK_EXECUTABLE_PATH"
        mv "$FRAMEWORK_EXECUTABLE_PATH-merged" "$FRAMEWORK_EXECUTABLE_PATH"
    done
    ```
	
#### Android
The 'Minimum API Level' must be set to at least Android 4.0.3 'Ice Cream Sandwich' (API level 15).

# Testing Tips
Features to test
+ Detecting/linking with the Parent App
+ Retrieving the user's balance
+ Performing a purchase

# FAQ



---
If you have any questions or would like some help, send an email to support@itavio.com.

Take care,  
The Itavio Team  
http://www.itavio.com  
support@itavio.com
