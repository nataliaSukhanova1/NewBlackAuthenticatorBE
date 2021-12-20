<template>
    <div id="app">
        <img alt="Vue logo" src="./assets/kerst.gif">
        <HelloWorld msg="Welcome to New Black Authenticator" />
        <h3>Log in</h3>
        <LoginForm></LoginForm>
    </div>
</template>

<script>
    import HelloWorld from './components/HelloWorld.vue'
    import LoginForm from './components/LoginForm.vue'
   // import Vue from 'vue'
    //import { BootstrapVue } from 'bootstrap-vue'
   // Vue.use(BootstrapVue)

export default {
  name: 'App',
  components: {
      HelloWorld,
      LoginForm
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
