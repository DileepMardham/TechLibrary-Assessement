<template>
    <div class="home">
        <h1>{{ msg }}</h1>
        <h1 align="center">Technology Library</h1>

        <div class="list row">
            <div class="col-md-12">
                <div class="input-group mb-3">
                    <input type="text"
                           class="form-control"
                           placeholder="Search by title"
                           v-model="searchTerms" 
                           v-on:keyup.enter="onEnter"/>
                    <div class="input-group-append">
                        <button class="btn btn-outline-secondary"
                                type="button"
                                @click="currentPage = 1; searchBooks();">
                            Search
                        </button>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">               
                <b-pagination v-model="currentPage"
                              :total-rows="count"
                              :per-page="pageSize"
                              prev-text="Prev"
                              next-text="Next"
                              @change="handlePageChange"
                              size="lg"></b-pagination>
            </div>
           
            <div class="col-lg-12">
                <table class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>Book Title</th>
                            <th>ISBN</th>                            
                            <th>Description</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="book in books" :key="book.bookId" v-bind:class="{selected: book.bookId == currentIndex}">
                            <td>
                                <b-link :to="{ name: 'Book', params: { 'id' : book.bookId } }">{{ book.title }}</b-link>
                            </td>
                            <td>{{book.isbn}}</td>
                            <td>{{book.descr}}</td>
                            <td align="center">
                            <img v-bind:src="`${book.thumbnailUrl}`">
                            <br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
    //import axios from 'axios';
    import booksDataService from "../services/BooksDataService";

    export default {
        name: 'Home',
        props: {
            msg: String
        },
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'descr', label: 'Description', sortable: true, sortDirection: 'desc' }

            ],
            books: [],
            perPage: 3,
            currentPage: 1,
            currentBook: null,
            currentIndex: -1,
            searchTerms: "",
            pageSize: 10,
            count:0
        }),
        methods: {           
            getRequestParams(searchTerms, currentPage, pageSize) {
                let params = {};

                if (searchTerms) {
                    params["searchTerms"] = searchTerms;
                }

                if (currentPage) {
                    params["pageNumber"] = currentPage;
                }

                if (pageSize) {
                    params["pageSize"] = pageSize;
                }

                return params;
            },
            retrieveBooks() {
                const params = this.getRequestParams(
                    this.searchTerms,
                    this.currentPage,
                    this.pageSize
                );

                booksDataService.getAll(params)
                    .then((response) => {
                        const books = response.data;
                        this.books = books.data;
                        this.count = books.count;
                        console.log(response.data);
                    })
                    .catch((e) => {
                        console.log(e);
                    });
            },
            searchBooks() {
                const params = this.getRequestParams(
                    this.searchTerms,
                    this.currentPage,
                    this.pageSize
                );

                booksDataService.searchBooks(params)
                    .then((response) => {
                        const books = response.data;
                        this.books = books.data;
                        this.count = books.count;
                        console.log(response.data);
                    })
                    .catch((e) => {
                        console.log(e);
                    });
            },
            handlePageChange(value) {
                this.currentPage = value;
                this.retrieveBooks();
            },
            onEnter: function () {
                this.currentPage = 1;
                this.retrieveBooks();
            }
        },
        mounted: function(){
            this.retrieveBooks();
        },
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

