<script lang="ts">
import Navbar from './components/Navbar.vue';
import Footer from './components/Footer.vue';

export default {
  name: 'App',
  components: {Navbar, Footer}
}
</script>

<template>
  <Navbar/>
  <RouterView />
  <Footer/>
</template>
