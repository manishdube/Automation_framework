Given I opened the main page
When I trigger the FEPOC Notification as "A1C_2ND_HALF_TEST_NOT_COMPLTD" "R60378908" "14046128"
And The "MyBlue" page is successfully loaded
Given I input a username "SEAN3"
Given I input a password "Sittest1!"
When I click submit button
When I click the link for a different verification way
And I choose use my PIN
And I input the MyBlue PIN code "6128"
And I click the verify button
And I click continue button in the for 4 months dialogue
When I Navigate to Secure Message Center
And I verify the Secure Message Center loaded
And I search for 'Welcome to MyBlue' email subject
Then I open and confirm verbiage in email
And I sign off from MyBlue
