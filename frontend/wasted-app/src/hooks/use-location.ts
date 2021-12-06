import {useDispatch, useSelector} from "react-redux"
import {fetchLocation} from "../actions/location"
import {getLocation, isLocationAllowed, isLocationLoaded} from "../selectors/location"

export const useLocation = () => {
  const dispatch = useDispatch()

  return {
    location: useSelector(getLocation),
    locationAllowed: useSelector(isLocationAllowed),
    locationLoaded: useSelector(isLocationLoaded),
    fetchLocation: () => dispatch(fetchLocation())
  }
}
