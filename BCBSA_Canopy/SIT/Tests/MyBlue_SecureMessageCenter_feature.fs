module MyBlue_SecureMessageCenter_feature
open canopy

let all _ = 
    context "MyBlue_SecureMessageCenter_feature"

    "Given I opened the main page" &&&& fun _ ->
        url ("https://stage.fepblue.org:8443/pilot/login") 
        on ("https://stage.fepblue.org:8443/pilot/login")
    "And The 'MyBlue' page is successfully loaded" &&&& fun _ ->
        displayed "Welcome to MyBlue"
    "Given I input a username 'SEAN3'" &&&& fun _ ->
        "#LoginUsername" << "SEAN3"
    "Given I input a password 'Sittest1!'" &&&& fun _ ->
        "#LoginPassword" << "Sittest1!"
    " When I click submit button" &&&& fun _ ->
        click "#login"
    "When I click the link for a different verification way" &&&& fun _ ->
        click "Continue"
        click "Verify your device another way"
    "And I choose use my PIN" &&&& fun _ ->
        click "Use my PIN"
    "And I input the MyBlue PIN code '6128'" &&&& fun _ ->
        ".csr-input" << "6128"
    "And I click the verify button" &&&& fun _ ->
        click "Verify"
    "And I click continue button in the for 4 months dialogue" &&&& fun _ ->
        click "Continue"
        displayed "body > div.modal.fade.ng-scope.ng-isolate-scope.in > div > div > sms-collection-modal > div > div.sms-collection-footer > a.btn.btn-default.add-or-confirm-number"
        click "Not Now"
        notDisplayed "body > div.modal.fade.ng-scope.ng-isolate-scope.in > div > div > sms-collection-modal > div > div.sms-collection-footer > a.btn.btn-default.add-or-confirm-number"
    "When I Navigate to Secure Message Center" &&&& fun _ ->
        hover "#Mailbox > a"
        click "Secure Message Center"
    "And I verify the Secure Message Center loaded" &&&& fun _ ->
        displayed "Secure Message Center"
    "And I search for 'Welcome to MyBlue' email subject" &&&& fun _ ->
        "#searchBox" << "Welcome to MyBlue"
        click "#searchBtn"
    "Then I open and confirm verbiage in email" &&&& fun _ ->
         click "#openMsg01 > span:nth-child(1)"
         displayed "Welcome Sean!"
    "And I sign off from MyBlue" &&&& fun _ ->
        click "#slide-panel-msg03 > div.toolbar > a:nth-child(1) > span"
        click "#Username"
        click "Sign Out" 
        displayed "Welcome to MyBlue"
