<template>
      <router-view  @userlogin="userlogin" :user="user" @userLogOut="userLogOut"></router-view>
</template>
<script>

export default {
    name: "App",
    emits: ["userlogin"],
    created() {
      
        if(!this.isUser){
          this.$router.push('/login');
        }else{
          this.$router.push('/home/total');
        }
    },
    methods: {
      userlogin(e){
        this.user.nameOfUser = e.nameOfUser;
        this.user.role = e.role;
        this.isUser = true;
      },
        /**
         * Ẩn tooltip
         * author: LTQN(2/10/2022)
         */
        hideTooltip() {
            this.isShowTooltip = false;
            this.posXTooltip = 0;
            this.posYTooltip = 0;
        },
        /**
         * Hiện tooltip
         * author: LTQN(2/10/2022)
         * @param {objec} e : nội dung/vị trí
         */
        showTooltip(e) {
            this.contentToolTip = e.contentTooltip;
            this.posXTooltip = e.positionX;
            this.posYTooltip = e.positionY;
            this.isShowTooltip = true;
        },
        userLogOut(){
          this.$router.push('/login');
          this.$router.go(0);
        }
    },
    data() {
        return {
            contentToolTip: null,
            isShowTooltip: false,
            posYTooltip: 0,
            posXTooltip: 0,
            isUser: false,
            user: {
              nameOfUser: '',
              role: null

            }
   
        };
    },
    watch:{
      isUser(){
        if(!this.isUser){
          this.$router.push('/login');
        }else{
          this.$router.push('/total');
        }
      }
    },
    components: { }
}
</script>

<style>
  @import url(../src/css/main.css);
  @import url(../src/css/base/input.css);


</style>
