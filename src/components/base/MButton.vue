<template>
    <button :disabled="disablebutton" :class="{'disablebutton':disablebutton}" class="button">{{text}}</button>
</template>
  
<script>
  export default {
    props: {
        //button thực hiện chức năng gì
        text: String,
        disablebutton: Boolean
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/button.css);
</style>
  