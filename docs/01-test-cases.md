Below is the **Selenium C# conversion** of your test cases, keeping your original structure and wording as much as possible. I’ve also separated the final E2E suite so you can directly use it as a test-case document for a Selenium C# project.

# 1. Login Page Test Cases

## UI Test Cases

| ID          | Test Case                                                                        | Expected Result                                                                  |
| ----------- | -------------------------------------------------------------------------------- | -------------------------------------------------------------------------------- |
| LOGIN-UI-01 | Should display all essential login elements when the login page loads            | Username, password, login button, logo and other required elements are displayed |
| LOGIN-UI-02 | Should render responsively on mobile viewports                                   | Login page renders correctly without layout issues                               |
| LOGIN-UI-03 | Should change the input border color to red when an error state is active        | Invalid input displays the expected error styling                                |
| LOGIN-UI-04 | Should display the error container bar at the bottom of the form                 | Error container is displayed with the appropriate error message                  |
| LOGIN-UI-05 | Should login with valid credentials                                              | User is redirected to the inventory page                                         |
| LOGIN-UI-06 | Should display an error message when a locked-out user's credentials are entered | Locked-out error message is displayed                                            |
| LOGIN-UI-07 | Should display an error message when an invalid username is entered              | Invalid username error message is displayed                                      |
| LOGIN-UI-08 | Should display an error message when an invalid password is entered              | Invalid password error message is displayed                                      |
| LOGIN-UI-09 | Should clear the error message when clicking the close X button                  | Error container disappears                                                       |

## E2E Test Cases

| ID           | Test Case                                                                  | Expected Result                            |
| ------------ | -------------------------------------------------------------------------- | ------------------------------------------ |
| LOGIN-E2E-01 | Should prevent access to the inventory page when the user is not logged in | User is redirected to the login page       |
| LOGIN-E2E-02 | Should redirect to the login page after an explicit user logout            | User is logged out and redirected to login |

---

# 2. Inventory Page Test Cases

## UI Test Cases

| ID        | Test Case                                                                                    | Expected Result                                            |
| --------- | -------------------------------------------------------------------------------------------- | ---------------------------------------------------------- |
| INV-UI-01 | Should display the correct product information                                               | Product name, description, price and image are correct     |
| INV-UI-02 | Should add a single product to the cart and update the badge count to 1                      | Product is added and cart badge displays `1`               |
| INV-UI-03 | Should toggle the button text from Add to cart to Remove when a product is added and removed | Button changes appropriately                               |
| INV-UI-04 | Should sort product names in descending alphabetical order Z to A                            | Products are sorted Z → A                                  |
| INV-UI-05 | Should sort product names in ascending alphabetical order A to Z                             | Products are sorted A → Z                                  |
| INV-UI-06 | Should sort products by price from low to high                                               | Products are sorted by ascending price                     |
| INV-UI-07 | Should sort products by price from high to low                                               | Products are sorted by descending price                    |
| INV-UI-08 | Should remove all products from the cart and ensure the badge is hidden                      | Cart becomes empty and badge disappears                    |
| INV-UI-09 | Should display matching images, titles and prices for all inventory cards                    | Every inventory card contains matching product information |
| INV-UI-10 | Should retain added cart items after reloading the inventory page                            | Previously added products remain in the cart               |

## E2E Test Cases

| ID         | Test Case                                                                                   | Expected Result                      |
| ---------- | ------------------------------------------------------------------------------------------- | ------------------------------------ |
| INV-E2E-01 | Should open the detailed item view when clicking on a product title                         | Product detail page opens            |
| INV-E2E-02 | Should maintain the cart badge count when navigating to the cart page and back to inventory | Cart badge retains the correct count |

---

# 3. Cart Page Test Cases

## UI Test Cases

| ID         | Test Case                                                                      | Expected Result                           |
| ---------- | ------------------------------------------------------------------------------ | ----------------------------------------- |
| CART-UI-01 | Should display correct product names and prices for all items in the cart list | Cart displays correct product information |
| CART-UI-02 | Should verify Sauce Labs Backpack quantity is 1 in the cart                    | Backpack quantity is `1`                  |
| CART-UI-03 | Should display the cart page title as Your Cart                                | `Your Cart` is displayed                  |
| CART-UI-04 | Should display Continue Shopping and Checkout buttons on the cart page         | Both buttons are visible                  |
| CART-UI-05 | Should allow the user to remove an item directly from the cart page list       | Selected product is removed               |

## E2E Test Cases

| ID          | Test Case                                                                             | Expected Result                 |
| ----------- | ------------------------------------------------------------------------------------- | ------------------------------- |
| CART-E2E-01 | Should navigate to the checkout page when clicking the Checkout button                | Checkout information page opens |
| CART-E2E-02 | Should navigate back to the inventory page when clicking the Continue Shopping button | Inventory page opens            |

---

# 4. Checkout Page Test Cases

## UI Test Cases

| ID          | Test Case                                                                           | Expected Result                           |
| ----------- | ----------------------------------------------------------------------------------- | ----------------------------------------- |
| CHECK-UI-01 | Should highlight input fields with error icons when validation fails                | Error icons/styling are displayed         |
| CHECK-UI-02 | Should display placeholder text inside first name, last name and postal code inputs | Correct placeholders are displayed        |
| CHECK-UI-03 | Should show an error when required fields are empty                                 | Validation error is displayed             |
| CHECK-UI-04 | Should show a validation error when only the postal code field is missing           | Postal-code validation error is displayed |
| CHECK-UI-05 | Should calculate the total price correctly                                          | Calculated total matches expected amount  |

## E2E Test Cases

| ID           | Test Case                                                                          | Expected Result                      |
| ------------ | ---------------------------------------------------------------------------------- | ------------------------------------ |
| CHECK-E2E-01 | Should complete the full checkout process                                          | User successfully completes checkout |
| CHECK-E2E-02 | Should navigate to checkout overview when clicking Continue with valid information | Checkout overview page opens         |
| CHECK-E2E-03 | Should return to the cart page when clicking Cancel on checkout information page   | Cart page opens                      |

---

# 5. Checkout Overview / Payments Page Test Cases

I'd call this **Checkout Overview** in the Selenium project because that's how SauceDemo actually models this part of the flow.

## UI Test Cases

| ID             | Test Case                                                                | Expected Result                                          |
| -------------- | ------------------------------------------------------------------------ | -------------------------------------------------------- |
| OVERVIEW-UI-01 | Should format item total, tax and grand total currencies correctly       | Currency values have correct formatting                  |
| OVERVIEW-UI-02 | Should display shipping information and payment card masking placeholder | Payment and shipping information are displayed           |
| OVERVIEW-UI-03 | Should display item quantities and descriptions matching cart contents   | Overview matches the cart                                |
| OVERVIEW-UI-04 | Should display item total without floating-point rounding artifacts      | Total is displayed correctly without precision artifacts |

## E2E Test Cases

| ID              | Test Case                                                                     | Expected Result                     |
| --------------- | ----------------------------------------------------------------------------- | ----------------------------------- |
| OVERVIEW-E2E-01 | Should match the grand total mathematical sum of items plus calculated tax    | Grand total equals item total + tax |
| OVERVIEW-E2E-02 | Should return to the inventory page when clicking Cancel on checkout overview | User returns to inventory           |

---

# 6. Order Complete Page Test Cases

## UI Test Cases

| ID             | Test Case                                              | Expected Result                           |
| -------------- | ------------------------------------------------------ | ----------------------------------------- |
| COMPLETE-UI-01 | Should display Checkout: Complete! page title          | Correct page title is displayed           |
| COMPLETE-UI-02 | Should display the green checkmark confirmation icon   | Confirmation icon is visible              |
| COMPLETE-UI-03 | Should display Thank you for your order! heading       | Correct confirmation heading is displayed |
| COMPLETE-UI-04 | Should display the order dispatch confirmation message | Dispatch message is displayed             |
| COMPLETE-UI-05 | Should display the Back Home button                    | Back Home button is visible               |

## E2E Test Cases

| ID              | Test Case                                                                    | Expected Result                   |
| --------------- | ---------------------------------------------------------------------------- | --------------------------------- |
| COMPLETE-E2E-01 | Should reach the order complete page after finishing the checkout overview   | Order Complete page is displayed  |
| COMPLETE-E2E-02 | Should completely clear the shopping cart badge count after order completion | Cart badge is no longer displayed |
| COMPLETE-E2E-03 | Should navigate to the inventory page when clicking Back Home                | Inventory page opens              |

---

# 7. Selenium C# — Full E2E Test Suite

For the E2E automation specifically, I would use these as your main **Selenium C# end-to-end scenarios**.

| ID     | E2E Scenario                                 | Selenium Flow                                                                                               | Expected Result                               |
| ------ | -------------------------------------------- | ----------------------------------------------------------------------------------------------------------- | --------------------------------------------- |
| E2E-01 | Successful single-product purchase           | Login → Select product → Add to cart → Cart → Checkout → Enter details → Continue → Finish                  | Order successfully placed                     |
| E2E-02 | Successful multi-product purchase            | Login → Add product A → Add product B → Cart → Checkout → Finish                                            | All selected products are purchased           |
| E2E-03 | Purchase after removing an item              | Login → Add A → Add B → Cart → Remove A → Checkout → Finish                                                 | Only B is purchased                           |
| E2E-04 | Purchase after changing product selection    | Login → Add A → Remove A → Add B → Checkout → Finish                                                        | B is successfully purchased                   |
| E2E-05 | Continue shopping before checkout            | Login → Add A → Cart → Continue Shopping → Add B → Cart → Checkout → Finish                                 | A and B are purchased                         |
| E2E-06 | Cart persistence through checkout            | Login → Add product → Cart → Checkout → Cancel → Cart → Checkout → Finish                                   | Product remains in cart and purchase succeeds |
| E2E-07 | Cancel checkout and resume purchase          | Login → Add product → Cart → Checkout → Cancel → Checkout again → Finish                                    | Purchase completes successfully               |
| E2E-08 | Complete purchase with product sorting       | Login → Sort products → Select product → Add to cart → Checkout → Finish                                    | Sorted product is successfully purchased      |
| E2E-09 | Locked-out user purchase attempt             | Login with locked-out user → Attempt login                                                                  | User cannot access inventory                  |
| E2E-10 | Session logout during shopping               | Login → Add product → Logout → Login again → Continue shopping                                              | User can resume expected workflow             |
| E2E-11 | Purchase workflow with browser refreshes     | Login → Add product → Cart → Refresh → Checkout → Refresh → Finish                                          | Workflow remains functional                   |
| E2E-12 | Complete purchase using different user types | Login with supported user → Add product → Checkout → Finish                                                 | Workflow behaves according to user type       |
| E2E-13 | Cart modification during checkout workflow   | Login → Add A + B → Checkout → Cancel → Cart → Remove B → Checkout → Finish                                 | Final cart contents are purchased             |
| E2E-14 | Multiple sequential purchases                | Login → Purchase A → Back Home → Purchase B                                                                 | Both purchase workflows complete              |
| E2E-15 | Full customer journey                        | Login → Browse → View product → Add products → Modify cart → Checkout → Complete order → Back Home → Logout | Complete customer journey succeeds            |

## Recommended Selenium C# naming

For your actual test methods, I'd use **PascalCase + descriptive behavior**, for example:

```csharp
[Test]
public void ShouldCompleteSingleProductPurchase()
{
}

[Test]
public void ShouldCompleteMultiProductPurchase()
{
}

[Test]
public void ShouldPurchaseRemainingProductAfterRemovingItem()
{
}

[Test]
public void ShouldCompletePurchaseAfterSortingProducts()
{
}

[Test]
public void ShouldPreventLockedOutUserFromPurchasing()
{
}

[Test]
public void ShouldMaintainCartAfterRefreshingInventoryPage()
{
}

[Test]
public void ShouldCompleteMultipleSequentialPurchases()
{
}
```

For a **Selenium C# framework**, I'd keep the separation roughly:

```text
SauceDemo.Tests
│
├── Tests
│   ├── LoginTests.cs
│   ├── InventoryTests.cs
│   ├── CartTests.cs
│   ├── CheckoutTests.cs
│   ├── CheckoutOverviewTests.cs
│   ├── OrderCompleteTests.cs
│   └── E2ETests.cs
│
├── Pages
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   ├── CartPage.cs
│   ├── CheckoutPage.cs
│   ├── CheckoutOverviewPage.cs
│   └── OrderCompletePage.cs
│
├── Utilities
├── TestData
└── Drivers
```

**One correction to your original categorization:** the "Payments page" on SauceDemo is more accurately the **Checkout Overview** page, since SauceDemo doesn't have a separate payment-entry page. This naming will make your Selenium C# framework cleaner and closer to the application's actual page flow.
