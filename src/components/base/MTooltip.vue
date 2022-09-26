<template>
    <div class="tooltip"  :class="stand">
      <div class="tooltip-content">{{content}}</div>
      <div class="arrow"></div>
    </div>
  </template>
  
<script>
export default {
  props: {
    content: String,
    stand: String //đứng trên/dưới/trái/phải
  }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/tooltip.css);
</style>
  