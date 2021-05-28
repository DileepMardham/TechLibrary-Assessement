import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const Home = () => import(/* webpackChunkName: "Home" */ './components/Home.vue');
const Book = () => import(/* webpackChunkName: "Book" */ './components/Book.vue');
const AddBook = () => import(/* webpackChunkName: "AddBook" */ './components/AddBook.vue');

const router = new VueRouter({
    routes: [
        {
            path: '/',
            name: "Home",
            component: Home
        },
        {
            name: 'Book',
            path: '/book/:id',
            component: Book,
            props: true
        },
        {
            path: "/AddBook",
            name: "AddBook",
            component: AddBook
        }
    ]
});

export default router;