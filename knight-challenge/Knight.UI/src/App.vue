<script lang="ts">
import Navbar from './components/Navbar.vue';
import Footer from './components/Footer.vue';
import Knights from './components/Knights.vue';

export default {
  name: 'App',
  components: {Navbar, Knights, Footer}
}
</script>

<template>
  <Navbar/>
  <Knights/>
  <Footer/>
</template>
