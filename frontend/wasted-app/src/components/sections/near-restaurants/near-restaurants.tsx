import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant, RestaurantSortType} from "../../../api/interfaces"
import {navigateToRestaurantInfo} from "../../../services/navigation"
import {formatDistance} from "../../../utils/coordinates"
import {HorizontalList} from "../../horizontal-list"
import {HorizontalListItem} from "../horizontal-list-item"
import {NearRestaurantsProps} from "./interfaces"

export const NearRestaurants = ({componentId, location}: NearRestaurantsProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    setRestaurants(
      await getAllRestaurants({
        sortObject: {
          sortType: RestaurantSortType.DIST,
          coordinates: {longitude: location.longitude, latitude: location.latitude}
        }
      })
    )
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <HorizontalListItem
      name={item.name}
      imageURL={item.imageURL}
      tag={`${formatDistance(item.distanceToUser)} km`}
      onPress={() => navigateToRestaurantInfo(componentId, {componentId, restaurant: item})}
    />
  )

  useEffect(() => {
    fetchRestaurants()
  }, [])

  return (
    <View centerV margin-s4>
      <Text text50L marginB-s2>
        📍 Restaurants near you
      </Text>
      <HorizontalList items={restaurants} renderItem={renderItem} />
    </View>
  )
}
