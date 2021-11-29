import {useDispatch, useSelector} from "react-redux"
import {fetchLocation} from "../actions/location"
import {getLocation, isLocationLoaded} from "../selectors/location"

export const useLocation = () => {
  const dispatch = useDispatch()

  return {
    location: useSelector(getLocation),
    locationLoaded: useSelector(isLocationLoaded),
    fetchLocation: () => dispatch(fetchLocation())
  }
}
