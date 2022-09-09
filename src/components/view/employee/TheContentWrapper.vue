<template>
    <div class="content-wrapper">
        <TheTopnav/>
        <TheContent/>
    </div>
  </template>
  
<script>
import TheTopnav from '@/components/layout/TheTopnav.vue';
import TheContent from '@/components/layout/TheContent.vue';
  export default {
    components: { TheTopnav, TheContent }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .content-wrapper{
    height: 100%;
    width: calc(100% - 200px);
}
</style>
  