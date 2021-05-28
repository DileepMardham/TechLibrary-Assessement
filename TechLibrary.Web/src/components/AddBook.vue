<template>
    <div class="submit-form">
        <div v-if="!submitted">
            <div class="form-group">
                <label for="title">Title</label>
                <input type="text"
                       class="form-control"
                       id="title"
                       required
                       v-model="book.title"
                       name="title" />
            </div>

            <div class="form-group">
                <label for="description">Description</label>
                <input class="form-control"
                       id="description"
                       v-model="book.descr"
                       name="description" />
            </div>

            <div class="form-group">
                <label for="isbn">ISBN</label>
                <input class="form-control"
                       id="isbn"
                       required
                       v-model="book.isbn"
                       name="isbn" />
            </div>

            <div class="form-group">
                <label for="publishedDate">Published Date</label>

                <b-form-datepicker class="form-control"
                                   id="publishedDate"
                                   v-model="book.publishedDate"
                                   required
                                   name="publishedDate"></b-form-datepicker>
            </div>

            <div class="form-group">
                <label for="thumbnailUrl">Thumbnail Url</label>
                <input class="form-control"
                       id="thumbnailUrl"
                       required
                       v-model="book.thumbnailUrl"
                       name="thumbnailUrl" />
            </div>

            <div>
                <div id="preview">
                    <img v-if="book.thumbnailUrl" :src="book.thumbnailUrl" />
                </div>
            </div>

            <button @click="saveBook" class="btn btn-success">Submit</button>
        </div>

        <div v-else>
            <h4>You submitted successfully!</h4>
            <button class="btn btn-success" @click="newBook">Add</button>
        </div>
    </div>
</template>

<script>
    import booksDataService from "../services/BooksDataService";

    export default {
        name: "AddBook",
        data() {
            return {
                book: {
                    id: null,
                    title: "",
                    isbn: "",
                    description: "",
                    published: "",
                    thumbnailUrl:""
                },
                submitted: false
            };
        },
        methods: {
            saveBook() {
                var data = {
                    title: this.book.title,
                    description: this.book.description
                };

                booksDataService.create(data)
                    .then(response => {
                        this.book.id = response.data.id;
                        console.log(response.data);
                        this.submitted = true;
                    })
                    .catch(e => {
                        console.log(e);
                    });
            },

            newBook() {
                this.submitted = false;
                this.book = {};
            }
        }
    };
</script>

<style>
    .submit-form {
        max-width: 1200px;
        margin: auto;
    }
</style>