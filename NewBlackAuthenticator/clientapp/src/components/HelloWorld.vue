<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    
    
    <h3>All users table</h3>
    <template>
        <div>
            <b-table striped hover :items="users"  :fields="fields"></b-table>
        </div>
    </template>
  </div>

</template>

<script>
    import axios from 'axios';

    export default {

        data() {
            return {
                fields: ['username', 'password','device_token1', 'device_token2'],
                users: [],
                errors: [],
            }
        },
        name: 'HelloWorld',
        components: {

        },
        props: {
            msg: String
        },
        methods: {
            async getUsers() {
                try {
                    const response = await axios.get(`https://localhost:44346/api/users`)
                    console.log(response.data)
                }
                catch (e) {
                    this.errors.push(e)
                }
            }
        },  
        mounted() {
            axios.get('https://localhost:44346/api/users')
                .then(function (response) {
                    console.log(response);
                })
        }
    }

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
