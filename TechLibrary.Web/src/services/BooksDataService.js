import http from "../http-common";

class BooksDataService {
    getAll(params) {
        return http.get("https://localhost:5001/api/books", { params });
    }

    searchBooks(params) {
        return http.get("https://localhost:5001/api/books", { params });
    }

    get(id) {
        return http.get(`https://localhost:5001/api/books/${id}`);
    }

    create(data) {
        return http.post("https://localhost:5001/api/books/AddBook", data);
    }

    update(id, data) {
        return http.put(`https://localhost:5001/api/books/UpdateBook`, data);
    }

    delete(id) {
        return http.delete(`https://localhost:5001/api/books/DeleteBook/${id}`);
    }

}

export default new BooksDataService();