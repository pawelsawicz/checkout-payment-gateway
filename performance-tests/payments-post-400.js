import http from "k6/http";

export default function() {
    var url = "http://localhost:5000/api/payments";
    var payload = JSON.stringify(
        {
            CardNumber: "aaaaaaaaa",
            ExpiryMonth: 6,
            ExpiryDate: 2018,
            Name: "aaaaa",
            Amount: 2000,
            CurrencyCode: "USD",
            Cvv: 443
        }
    );
    var params =  { headers: { "Content-Type": "application/json" } }
    http.post(url, payload, params);
};