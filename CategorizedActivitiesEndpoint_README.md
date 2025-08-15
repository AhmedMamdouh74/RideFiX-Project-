# Categorized Activities Endpoint

This endpoint provides a comprehensive view of all system activities organized by categories, making it easy for administrators to monitor different types of activities separately.

## Endpoint Details

### URL
```
GET /api/Admin/activities?hoursBack=12
```

### Location
- **Controller**: `AdminController`
- **Method**: `GetCategorizedActivities`
- **Authorization**: Requires admin access

### Parameters
- `hoursBack` (optional): Number of hours to look back (1-168, default: 12)

## Response Structure

The endpoint returns activities organized into 5 main categories:

### 1. EmergencyRequests
Activities related to emergency service requests:
- **Created**: New emergency requests
- **Completed**: Completed emergency requests  
- **Updated**: Status updates on emergency requests

### 2. CarMaintenanceRecords
Activities related to car maintenance:
- **Completed**: Completed maintenance records

### 3. UserRegistrations
Activities related to user registrations:
- **Registered**: New technician registrations
- **Registered**: New car owner registrations

### 4. Reviews
Activities related to reviews:
- **Created**: New reviews added to the system

### 5. ChatSessions
Activities related to chat sessions:
- **Created**: New chat sessions initiated

## Sample Response

```json
{
  "success": true,
  "message": "تم جلب النشاطات المصنفة بنجاح",
  "data": {
    "emergencyRequests": [
      {
        "activityType": "Created",
        "description": "تم إنشاء طلب خدمة طوارئ جديد",
        "timestamp": "2024-01-15T10:30:00Z",
        "timeAgo": "منذ 5 دقائق",
        "entityType": "EmergencyRequest",
        "entityId": 123
      },
      {
        "activityType": "Completed",
        "description": "تم إكمال طلب خدمة طوارئ",
        "timestamp": "2024-01-15T09:15:00Z",
        "timeAgo": "منذ ساعة",
        "entityType": "EmergencyRequest",
        "entityId": 122
      }
    ],
    "carMaintenanceRecords": [
      {
        "activityType": "Completed",
        "description": "تم إكمال طلب صيانة",
        "timestamp": "2024-01-15T08:45:00Z",
        "timeAgo": "منذ ساعتين",
        "entityType": "CarMaintenanceRecord",
        "entityId": 45
      }
    ],
    "userRegistrations": [
      {
        "activityType": "Registered",
        "description": "تم تسجيل فني جديد في النظام",
        "timestamp": "2024-01-15T07:30:00Z",
        "timeAgo": "منذ 3 ساعات",
        "entityType": "Technician",
        "entityId": 67
      },
      {
        "activityType": "Registered",
        "description": "تم إضافة صاحب سيارة جديد",
        "timestamp": "2024-01-15T06:20:00Z",
        "timeAgo": "منذ 4 ساعات",
        "entityType": "CarOwner",
        "entityId": 89
      }
    ],
    "reviews": [
      {
        "activityType": "Created",
        "description": "تم إضافة تقييم جديد",
        "timestamp": "2024-01-15T05:10:00Z",
        "timeAgo": "منذ 5 ساعات",
        "entityType": "Review",
        "entityId": 34
      }
    ],
    "chatSessions": [
      {
        "activityType": "Created",
        "description": "تم إنشاء جلسة محادثة جديدة",
        "timestamp": "2024-01-15T04:30:00Z",
        "timeAgo": "منذ 6 ساعات",
        "entityType": "ChatSession",
        "entityId": 56
      }
    ],
    "totalCount": 6,
    "reportGeneratedAt": "2024-01-15T10:35:00Z"
  }
}
```

## Usage Examples

### Get activities for the last 12 hours (default)
```http
GET /api/Admin/activities
```

### Get activities for the last 24 hours
```http
GET /api/Admin/activities?hoursBack=24
```

### Get activities for the last 7 days
```http
GET /api/Admin/activities?hoursBack=168
```

## Implementation Details

### Service Method
- **Method**: `GetCategorizedActivitiesAsync(int hoursBack = 12)`
- **Returns**: `CategorizedActivityReportDTO`
- **Performance**: Each category is queried separately with Take(12) limit

### Database Queries
The service performs separate optimized queries for each category:
1. **EmergencyRequests**: Queries `EmergencyRequests` table for TimeStamp and EndTimeStamp
2. **CarMaintenanceRecords**: Queries `CarMaintenanceRecords` table for PerformedAt
3. **UserRegistrations**: Queries `Technicians` and `CarOwners` tables with ApplicationUser joins
4. **Reviews**: Queries `Reviews` table for DateTime
5. **ChatSessions**: Queries `ChatSessions` table for StartAt

### Time Calculation
- Uses `TimeHelper.CalculateTimeAgoInArabic()` for Arabic time formatting
- Provides human-readable time differences (منذ 5 دقائق, منذ ساعة, etc.)

## Benefits

### For Administrators
- **Organized View**: Activities are clearly separated by type
- **Quick Overview**: Easy to see which areas are most active
- **Detailed Monitoring**: Can focus on specific activity types
- **Arabic Support**: All descriptions and time formats in Arabic

### For Frontend Development
- **Structured Data**: Easy to create category-based UI components
- **Flexible Display**: Can show all categories or filter specific ones
- **Consistent Format**: All activities follow the same structure
- **Rich Information**: Includes entity IDs for detailed views

## Error Handling

### Validation Errors
- **Invalid hoursBack**: Returns 400 Bad Request if hoursBack < 1 or > 168
- **Clear Messages**: Error messages in Arabic for better UX

### System Errors
- **Database Errors**: Returns 500 Internal Server Error with details
- **Exception Handling**: Proper try-catch with meaningful error messages

## Security
- **Authorization Required**: Only accessible to admin users
- **Input Validation**: Validates time range parameters
- **Safe Queries**: Uses parameterized queries to prevent injection

## Performance Considerations
- **Optimized Queries**: Each category uses efficient LINQ expressions
- **Limited Results**: Each category limited to 12 most recent activities
- **Async Operations**: All database operations are async
- **Efficient Joins**: Uses Include() for related data when needed

