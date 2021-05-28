<template>
   
    <div v-if="book" class="edit-form">
        <h4>Book</h4>
        <form>
            <div class="form-group">
                <label for="title">Title</label>
                <input type="text" class="form-control" id="title"
                       v-model="book.title" />
            </div>
            <div class="form-group">
                <label for="description">Description</label>
                <input type="text" class="form-control" id="description"
                       v-model="book.descr" />
            </div>

            <div class="form-group">
                <label for="description">Published Date</label>
                <b-form-datepicker id="publishedDate" v-model="book.publishedDate"></b-form-datepicker>
            </div>

            <div class="form-group">
                <label for="description">Thumbnail Url</label>
                <input type="text" class="form-control" id="thumbnailUrl"
                       v-model="book.thumbnailUrl" />
            </div>

            <div>
                <div id="preview">
                    <img v-if="book.thumbnailUrl" :src="book.thumbnailUrl" />
                </div>
            </div>

        </form>

        <button class="badge badge-danger mr-4"
                @click="deleteBook">
            Delete
        </button>

        <button type="submit" class="badge badge-success"
                @click="updateBook">
            Update
        </button>



        <p>{{ message }}</p>
    </div>

    <div v-else>
        <br />
        <p>Please click on a Book...</p>
    </div>

</template>

<script>
    import booksDataService from "../services/BooksDataService";

    export default {
        name: 'Book',
        props: ["id"],
        data: () => ({
            book: null,
            message: ''
        }),
        methods: {
            getBook(id) {
                booksDataService.get(id)
                    .then(response => {
                        this.book = response.data;
                        console.log(response.data);
                    })
                    .catch(e => {
                        console.log(e);
                    });
            },
            updateBook() {
                booksDataService.update(this.book.bookId, this.book)
                    .then(response => {
                        console.log(response.data);
                        this.message = 'The book was updated successfully!';
                    })
                    .catch(e => {
                        console.log(e);
                    });
            },

            deleteBook() {
                booksDataService.delete(this.book.bookId)
                    .then(response => {
                        console.log(response.data);
                        this.$router.push({ name: "Home" });
                    })
                    .catch(e => {
                        console.log(e);
                    });
            },
            onFileChange(e) {
                const file = e.target.files[0];
                this.url = URL.createObjectURL(file);
            },
        },
        mounted() {
            this.message = '';
            this.getBook(this.$route.params.id);
        }
    }
</script>