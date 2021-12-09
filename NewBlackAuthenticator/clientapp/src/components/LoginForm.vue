    <template>
    <b-container fluiid="xs">

        <div>
            <b-form inline>
                <b-form-input id="inline-form-input-username" v-model="username" name="username"
                              class="mb-2 mr-sm-2 mb-sm-0" 
                              placeholder="Username"></b-form-input>
                <b-form-input id="inline-form-input-name" v-model="password" name="password"
                              class="mb-2 mr-sm-2 mb-sm-0"
                              placeholder="Password"></b-form-input>
                <b-button @click="checkUserCredentials()" variant="success">Log in</b-button>
            </b-form>
        </div>
    </b-container>

</template>

<script>
    import axios from 'axios';

export default {
  name: 'LoginForm',
  data() {
    return {
      username: '',
      password: '',
      //devicetoken1: '',
      //devicetoken2: ''
    }
  },
        methods: {
            async checkUserCredentials() {
                try {
                    await axios.post(`https://localhost:44346/api/accounts`, { username: this.username, password: this.password })
                        .then((response) => {
                            console.log(response);
                        })
                }
                catch (e) {
                    this.errors.push(e)
                }
                this.clearForm();
            },
      clearForm() {
          this.username = "";
          this.password = "";
      }
  }
}
</script>