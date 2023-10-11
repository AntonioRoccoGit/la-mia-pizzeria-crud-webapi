<script>
export default {
    name: "NavBar",
    data() {
        return {
            navlink: [
                {
                    routeName: "home",
                    label: "Home"
                },
                {
                    routeName: "about",
                    label: "About"
                }
            ]
        };
    }
};
</script>

<template>
    <nav class="ms_navbar">
        <div class="logo">
            <a href="">
                <h2>Logo</h2>

            </a>
        </div>
        <div class="action">
            <ul>
                <li v-for="item in navlink">
                    <router-link :to="{ name: item.routeName }" class="nav-link">
                        {{ item.label }}
                    </router-link>
                </li>

            </ul>
        </div>
    </nav>
</template>

<style scoped lang="scss">
//RESET
ul {
    list-style-type: none;
    display: flex;
    justify-content: right;
}

li:not(:first-child) {
    margin-left: 6px;
}

a {
    color: inherit;
    text-decoration: none;
}

.ms_navbar {
    border: 1px solid greenyellow;
    height: var(--navbar-height);
    display: flex;
    align-items: center;
    padding: 0 10px 0 10px;
}

.action {
    flex-grow: 1;
}
</style>