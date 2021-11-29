import {createReducer} from "@reduxjs/toolkit"
import {Location} from "react-native-get-location"
import {fetchLocation} from "../actions/location"

interface State {
  location: Location
  locationLoaded: boolean
}

export const initialState: State = {
  location: {
    latitude: 0,
    longitude: 0,
    accuracy: 0,
    altitude: 0,
    speed: 0,
    time: 0
  },
  locationLoaded: false
}

export const location = createReducer(initialState, builder => {
  builder.addCase(fetchLocation.fulfilled, (state, {payload}) => {
    state.location = payload
    state.locationLoaded = true
  })
})
