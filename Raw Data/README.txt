
DATA DESCRIPTION: 

 customer-data.csv DESCRIPTION:
   Each row corresponds to a transaction of a single item. Transactions with the same DATE and HOUSEHOLD_NUM constitute an entire purchase.

 customer-data.csv FIELDS:
   HOUSEHOLD_NUM: A unique number associated with a customer(s)
   HOUSE_LATITUDE: The latitude (in degrees) of the customer(s)
   HOUSE_LONGITUDE: The longitude (in degrees) of the customer(s)
   SEG_ID: The meaning of this field was lost with Willis L. Teeter
   DATE: A timestamp for when the transaction occurred
   STORE: A unique number for the store where the transaction occurred. See store-data.csv for STORE details
   DESCRIPTION: The item type purchase in transaction
   NET_SALES: The monetary value of the transaction


 store-data.csv FIELDS:
   STORE: A unique number associated with a specific Harris Teeter store
   LONGITUDE: The longitude (in degrees) of specific Harris Teeter store 
   LATITUDE: The latitude (in degrees) of the specific Harris Teeter store
