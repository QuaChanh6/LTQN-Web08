<template>
    <div :class="type" class="overView">
      <div class="icon-count-unit"></div>
      <div class="content-unit">
        <div class="content-text">{{text}}</div>
        <div class="content-number">{{number}}</div>
        <div class="icon-totalView">  <vue-fontawesome :icon='icon' size="5" color="black"></vue-fontawesome></div>
      </div>
    </div>
</template>
  
<script>

  export default {
    props: {
        text: String,
        number: Number,
        type: String,
        icon: String
    },
  
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/total/Total.css);
</style>
  