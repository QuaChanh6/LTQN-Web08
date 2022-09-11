<template>
    <div class="content-wrapper">
        <TheTopnav/>
        <TheContent @loadContent='loadContent' :key='reload'/>
    </div>
  </template>
  
<script>
import TheTopnav from '@/components/layout/TheTopnav.vue';
import TheContent from '@/components/layout/TheContent.vue';
  export default {
    components: { TheTopnav, TheContent },
    methods: {

      /**
       * Load lại component
       * author: LTQN(11/9/2022)
       * @param {INT} e Key mới
       */
      loadContent(e){
        this.reload = e;
      }
    },
    data(){
      return{
        reload: null
      }
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .content-wrapper{
    height: 100%;
    width: calc(100% - 200px);
}
</style>
  