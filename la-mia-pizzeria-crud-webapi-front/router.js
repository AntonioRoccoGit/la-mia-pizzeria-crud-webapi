import { createRouter, createWebHistory } from "vue-router";
import AppHome from "./src/pages/AppHome.vue"
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "home",
            component: AppHome
        },
        {
            path: "/about",
            name: "about",
            component: AppHome
        }
    ]
});

export { router };