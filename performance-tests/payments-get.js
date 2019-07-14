import http from "k6/http";

export default function() {
    for (var id = 1; id <= 5; id++) {
        http.get(http.url`http://localhost:5000/api/payments/${id}`)
    }
};