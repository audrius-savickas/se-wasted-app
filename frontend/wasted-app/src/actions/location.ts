import {createAsyncThunk} from "@reduxjs/toolkit"
import GetLocation, {Location} from "react-native-get-location"

export const fetchLocation = createAsyncThunk<Location, undefined>("GET_LOCATION", async () => {
  const location = await GetLocation.getCurrentPosition({
    enableHighAccuracy: true,
    timeout: 15000
  })
  return location
})
