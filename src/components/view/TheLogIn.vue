<template>
    <div class="login-page">
    <div class="login-form">
        <p class="title-login">ĐĂNG NHẬP</p>
      <input type="text" placeholder="username" v-model='user'/>
      <input type="password" placeholder="password" v-model = 'password'/>
      <span v-if="isShowValid" class="warning">Tên đăng nhập hoặc mật khẩu không đúng!</span>
      <button @click="login">login</button>
  </div>
    </div>
</template>
  
<script>
export default {
    emits: ["userlogin"],
    props:{
        load:Boolean
    },
    mounted(){
        if(sessionStorage.getItem("user") != undefined){
                this.$emit('userlogin', sessionStorage.getItem("user"));
            }
    },
    methods:{
        async login(){
            if(sessionStorage.getItem("user") != undefined){
                this.$emit('userlogin', sessionStorage.getItem("user"));
            }else{
                let url = process.env.VUE_APP_URL+'Users/login';

                let response = await fetch(url,{
                headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                method: "POST",
                body: JSON.stringify({UserName: this.user, PassWord: this.password})
                })
                if(response.ok){
                    let data =  await response.json();
                    sessionStorage.setItem('user', data);
                    this.$emit('userlogin', data);
                    this.$router.go(0);
                }else{
                    this.isShowValid= true;
                }
            }
            
        }
    },
    data(){
        return{
            user: '',
            password: '',
            key: 1,
            isShowValid: false
        }
    },
    watch:{
  
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/login.css);
.warning{
    color: red;
    margin-bottom: 12px;
    display: block;
}
</style>
  