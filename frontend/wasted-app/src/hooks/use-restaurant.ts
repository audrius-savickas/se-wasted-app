import {useDispatch, useSelector} from "react-redux"
import {logOutRestaurant, setRestaurantId} from "../actions/restaurant"
import {getRestaurantId} from "../selectors/restaurant"

export const useRestaurant = () => {
  const dispatch = useDispatch()

  return {
    restaurantId: useSelector(getRestaurantId),
    setRestaurantId: (restaurantId: string) => dispatch(setRestaurantId({id: restaurantId})),
    logOutRestaurant: () => dispatch(logOutRestaurant())
  }
}
