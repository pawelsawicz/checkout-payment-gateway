import http from "k6/http";

export default function() {
    
    var url = "http://api:80/api/payments";
    var payload = JSON.stringify(
        {
            CardNumber: "4702111549984688",
            ExpiryMonth: 6,
            ExpiryDate: 2019,
            Name: "aaaaa",
            Amount: 2000,
            CurrencyCode: "USD",
            Cvv: 443
        }
    );
    var params =  { headers: { "Content-Type": "application/json" } }
    var response = http.post(url, payload, params);

    http.get(http.url`http://api:80/api/payments/${JSON.parse(response.body).paymentId}`); 
};