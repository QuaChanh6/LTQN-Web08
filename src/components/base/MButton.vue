<template>
    <button class="button">{{text}}</button>
</template>
  
<script>
  export default {
    props: {
        //button thực hiện chức năng gì
        text: String
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/button.css);
</style>
  