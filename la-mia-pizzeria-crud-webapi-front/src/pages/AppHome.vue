<script>
import axios from 'axios';
import { store } from "../store.js";
export default {
    name: "AppHome",
    data() {
        return {
            store,
            pizzaList: []
        }
    },
    mounted() {
        this.getPizzas();
    },
    methods: {
        getPizzas() {
            axios
                .get(store.api.url + store.api.allPizza)
                .then(resp => {
                    this.pizzaList = resp.data.data;
                })
        }
    }
}
</script>

<template>
    <div class="ms_container">
        <h2>Qui puoi trovare tutte le nostre deliziose pizze!</h2>

        <!-- TO DO -->
        <!-- Barra di ricerca per titolo -->


        <div class="ms_row">
            <div v-for="item in pizzaList" class="card-body">
                <img :src="item.thumbnail" alt="">
                <section>
                    <h4>{{ item.name }}</h4>
                    <span>{{ item.description }}</span>
                </section>
            </div>
        </div>



    </div>
</template>

<style scoped lang="scss">
.ms_container {
    cursor: context-menu;
    padding: 2rem 0 2rem 0;
    text-align: center;
    width: 80%;
    max-width: 1400px;
    margin: 0 auto;
    border: 1px solid black;
}

.ms_row {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
    margin-top: 4rem;
}

.card-body {
    text-align: center;
    border: 1px solid black;
    border-radius: 15px;
    padding: .4rem 1rem 2rem 1rem;
    width: 23%;
    margin-bottom: 1rem;

    img {
        aspect-ratio: 3/2;
        border-radius: 15px;
        transition: 0.5s;

        &:hover {
            transform: scale(1.04);
        }
    }
}
</style>