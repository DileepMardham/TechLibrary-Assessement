<template>

    <div id="app">

    <nav class="navbar navbar-expand navbar-dark bg-dark">
        <router-link to="/" class="navbar-brand">Technology Library</router-link>
        <div class="navbar-nav mr-auto">
            <li class="nav-item">
                <router-link to="/" class="nav-link">Books</router-link>
            </li>
            <li class="nav-item">
                <router-link to="/AddBook" class="nav-link">Add Book</router-link>
            </li>
        </div>
    </nav>

    <b-container>
        <router-view>

        </router-view>
    </b-container>

    </div>

</template>

<script>

    export default {
        name: 'app',
    };
</script>

<style>
</style>

