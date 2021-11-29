import {Coordinates} from "../../api/interfaces"

export interface MapProps {
  coordinates: Coordinates
  coordinatesDelta: {
    latitudeDelta: number
    longitudeDelta: number
  }
  style: {
    height: number
  }
}
