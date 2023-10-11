<script>
import { RouterView } from 'vue-router';
import NavBar from './components/NavBar.vue';
export default {
  components: {
    NavBar,
    RouterView
  }
};
</script>

<template>
  <!-- HEADER -->
  <header>
    <NavBar />

  </header>
  <!-- /HEADER -->


  <!-- MAIN -->
  <main>
    <RouterView></RouterView>

  </main>
  <!-- /MAIN -->
</template>

<style scoped lang="scss">
header {
  border: 1px solid black
}
</style>
