import React from "react"
import MapView, {Marker, PROVIDER_GOOGLE} from "react-native-maps"
import {MapProps} from "./interfaces"

export const Map = ({coordinates, coordinatesDelta, style}: MapProps) => {
  return (
    <MapView provider={PROVIDER_GOOGLE} style={style} region={{...coordinates, ...coordinatesDelta}}>
      <Marker coordinate={coordinates} />
    </MapView>
  )
}
