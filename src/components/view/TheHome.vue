<template>
    <TheSidebar @hideTooltip= "hideTooltip" @showTooltip= "showTooltip" :name="name" :role="role" :code="code"/>
    <div class="box">
    <TheTopnav @hideTooltip= "hideTooltip" @showTooltip= "showTooltip" :nameOfUser="name" @userLogOut='userLogOut'/>
    <router-view  :code="code"></router-view>
    </div>
  </template>
  
  <script>
import TheSidebar from '../layout/TheSidebar.vue';
import TheTopnav from '../layout/TheTopnav.vue';
  export default {
    components: { TheSidebar, TheTopnav },
    props:{
      user: Object
    },
    created(){
      this.name = sessionStorage.getItem("user");
      this.role = sessionStorage.getItem("role");
      this.code = sessionStorage.getItem("code");
      if(this.code == "null"){
        this.code = "Quản trị hệ thống"
      }

    },
    data() {
        return {
          name: '',
          role: '',
          code: ''
        }
      },
    inheritAttrs: false,
    methods: {

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
          this.$emit('userLogOut', this.role);
        }
    },
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  
  </style>
  