import { reactive } from 'vue'

export const store = reactive({
    //My back-office
    api: {
        url: "https://localhost:7011/api",

        allPizza: "/PizzaApi/GetPizzas",
        pizzaByNameOrId: "/PizzaApi/GetPizza",
        createPizza: "/PizzaApi/Create",
        editPizza: "/PizzaApi/Edit"

    }

});