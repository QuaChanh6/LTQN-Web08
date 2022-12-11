<template>
    <TheSidebar @hideTooltip= "hideTooltip" @showTooltip= "showTooltip"/>
    <div class="box">
    <TheTopnav @hideTooltip= "hideTooltip" @showTooltip= "showTooltip" :nameOfUser="nameOfUser" @userLogOut='userLogOut'/>
    <router-view></router-view>
    </div>
  </template>
  
  <script>
import TheSidebar from '../layout/TheSidebar.vue';
import TheTopnav from '../layout/TheTopnav.vue';
  export default {
    components: { TheSidebar, TheTopnav },
    props:{
      nameOfUser:String
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
          this.$emit('userLogOut');
        }
    },
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  
  </style>
  