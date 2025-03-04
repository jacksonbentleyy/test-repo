let url = "http://localhost:5076/api/Book"
let myBooks = [];
async function handleOnLoad()
{
    await getAllBooks()
    generateBookTable()
}

async function getAllBooks()
{
    let response = await fetch(url);
    myBooks = await response.json();
    console.log(myBooks);
}

async function handleAddBook()
{

    const title = document.getElementById("title").value
    const author = document.getElementById("author").value
    const newBook = {title: title, author: author}
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newBook),
    });
    handleOnLoad();
}

function generateBookTable()
{
    const appDiv = document.getElementById("app");
    appDiv.innerHTML = "";

    const table = document.createElement("table");
    table.className = "table table-bordered";

    const thead = document.createElement("thead");
    thead.innerHTML = "<tr><th>Title</th><th>Author</th></tr>"
    table.appendChild(thead) // used to append something to the end of a div or a part of your html

    const tbody = document.createElement("tbody")
    myBooks.forEach((book) => {
        const row = document.createElement("tr")
        row.addEventListener("click", () => {
            handleEditBook(book)
        })
        row.innerHTML = `<td>${book.title}</td><td>${book.author}</td><td><button class="btn btn-danger"onclick="handleOnDelete(${book.id})">Delete</button></td>`
        tbody.appendChild(row)
    })

    table.appendChild(tbody)
    appDiv.appendChild(table)


}

function handleEditBook(book)
{
    const appDiv = document.getElementById("app")
    appDiv.innerHTML = `
    <h3>Edit book</h3>
    <input type="text" id="editTitle" value="${book.title}" class="form-control mb-2">
    <input type="text" id="editAuthor" value="${book.author}" class="form-control mb-2">
    <button class="btn btn-primary" onclick="saveEditBook(${book.id})>Save</button>
    <button class="btn btn-secondary" onclick="generateBookTable()>Cancel</button>
    `
}

async function saveEditedBook(bookId)
{
    const updatedBook = {
        id: bookId,
        title: document.getElementById("editTitle").value,
        author: document.getElementById("editAuthor").value
    }
    const response = await fetch(url + "/" + bookId + {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(updatedBook),
    });
    handleOnLoad();
}

function handleOnDelete(bookId)
{
    if(confirm("Are you sure you want to delete the book?")){
        console.log(bookId)
    }
    handleOnLoad()
}