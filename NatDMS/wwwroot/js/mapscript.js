function initialize() {
    var myLatlng = new google.maps.LatLng(12.9716, 77.5946);
  var mapProp = {
    center:myLatlng,
    zoom:10,
    mapTypeId:google.maps.MapTypeId.ROADMAP
      
  };
  var map=new google.maps.Map(document.getElementById("googleMap"), mapProp);
    var marker = new google.maps.Marker({
      position: myLatlng,
      map: map,
      title: 'Hello World!',
      draggable:true  
  });
    document.getElementById('lat').value= 12.9716
    document.getElementById('lng').value= 77.5946  
    // marker drag event
    google.maps.event.addListener(marker,'drag',function(event) {
        document.getElementById('lat').value = event.latLng.lat();
        document.getElementById('lng').value = event.latLng.lng();
    });

    //marker drag event end
    google.maps.event.addListener(marker,'dragend',function(event) {
        document.getElementById('lat').value = event.latLng.lat();
        document.getElementById('lng').value = event.latLng.lng();
        //alert("lat=>"+event.latLng.lat());
        //alert("long=>"+event.latLng.lng());
    });
}

google.maps.event.addDomListener(window, 'load', initialize);
