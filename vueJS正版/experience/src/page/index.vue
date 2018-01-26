<template>
  <div>index page
    <myContent></myContent>
  </div>
</template>

<script>
import myContent from './content'

export default {
  components: {
    myContent
  },
  created () {
    this.$api.get('topics', null, r => {
      console.log(r)
    })
  }
}

</script>
