# BeSpoked
 BeSpoked Bikes is looking to create a sales tracking application.  BeSpoked is a high-end bicycle shop and each salesperson gets a commission for each bike they sell.  They are introducing a new quarterly bonus based on sales as an incentive.

They are asking you to design a simple sales tracking application to help track the commission and determine each salesperson’s quarterly bonus.  


## Criteria
#### Data layer - Entities
- [x] Products – Name, Manufacturer, Style, Purchase Price, Sale Price, Qty On Hand, Commission Percentage
- [x]	Salesperson – First Name, Last Name, Address, Phone, Start Date, Termination Date, Manager
- [x]	Customer – First Name, Last Name, Address, Phone, Start Date
- [x]	Sales – Product, Salesperson, Customer, Sales Date
- [x]	Discount – Product, Begin Date, End Date, Discount Percentage
- [x]	Seed with sample data for testing

#### Middle tier
- [x] Allows for client access to the data layer

#### Client
- [x]	Display a list of salespersons
- [x]	Update a salesperson
- [x]	Display a list of products
- [x]	Update a product
- [x]	Display a list of customers
- [x]	Display a list of sales, including the Product, Customer, Date, Price, Salesperson, and Salesperson Commission.
- [x] Filter sales by date range.  
- [x] Create a sale
- [x] Display a quarterly salesperson commission report
- [x] Display a toggle to show the current quarter being shown 

#### Additional Requirements
- [x] No duplicate product can be entered. 
- [x] No duplicate salesperson can be entered. 

#### Non-functional Requirements
- [x] Publish the source code to an online source code repository of your choosing.
- [ ] ~~Optional: Host in Azure.~~

#### Stretch Goals
- [x] Additional styling
- [x] Date pickers (jQuery?)
- [x] Duplicate product and salesperson entries should be gracefully rejected with a user friendly error message
- [ ] ~~Repository with interface to separate the controllers and the DbContext class~~
