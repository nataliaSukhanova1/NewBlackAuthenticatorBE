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
                <b-button @click="login()" variant="success">Log in</b-button>
            </b-form>
        </div>
    </b-container>

</template>

<script>
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
      login() {
          console.log(this.username)
          console.log(this.password)

          this.clearForm();
      },
      clearForm() {
          this.username = "";
          this.password = "";
      }
  }
}
</script>