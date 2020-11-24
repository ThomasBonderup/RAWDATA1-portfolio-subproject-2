console.log("Before fetch");

let getCategories = function (callback) {
    fetch("api/categories")
        .then(function(response) {
            return response.json();
        })
        .then(function(data) {
            console.log(data);
        });
};

let createCategory = function(category, callback) {
    let headers = new Headers();
    headers.append("Content-Type", "application/json")
    fetch("api/categories", {method: "POST", body: JSON.stringify(category), headers})
        .then(response => response.json())
        .then(data => callback(data));
};

let deleteCategory = url => fetch(url, { method: "DELETE"});

getCategories(function(data) {
    console.log(data);
})

createCategory({name: 'sdfsdf', description: 'sdfafsdfjl'}, function(data) {
    console.log(data);
});

deleteCategory("api/categories/9");

console.log("After fetch");

