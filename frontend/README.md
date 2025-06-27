# Frontend 

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.1.6.

## Running the Frontend

Install dependencies:
```sh
npm install
```  

Start the development server:  
```sh
npm start
```

Open `http://localhost:4200/` in your browser.


The frontend expects a backend API running at `http://localhost:60992/`.
Ensure the backend is running and accessible at this address.
The frontend will authenticate using a `ClientId` and fetch a token before retrieving loan data.