import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import GetLocation, {Location} from "react-native-get-location"
import {Image, Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant, RestaurantSortType} from "../../../api/interfaces"
import {formatDistance} from "../../../utils/coordinates"
import {HorizontalList} from "../../horizontal-list"

export const NearRestaurants = () => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])

  const fetchRestaurants = async () => {
    const location = await GetLocation.getCurrentPosition({
      enableHighAccuracy: true,
      timeout: 15000
    })
    setRestaurants(
      await getAllRestaurants({
        sortType: RestaurantSortType.DIST,
        coordinates: {longitude: location.longitude, latitude: location.latitude}
      })
    )
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <View margin-s1 centerH>
      <Image
        source={{
          uri: item.imageURL,
          width: 100,
          height: 100
        }}
      />
      <Text marginT-s1>{item.name}</Text>
      <View br20 bg-purple30 padding-s1 paddingH-s2 marginT-s1>
        <Text white text90M>
          {`${formatDistance(item.distanceToUser)} km`}
        </Text>
      </View>
    </View>
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
