<template>
    <div class="tooltip">
      <div class="tooltip-content">{{content}}</div>
    </div>
</template>
  
<script>
export default {
  props: {
    content: String,
  },
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/tooltip.css);
</style>
  