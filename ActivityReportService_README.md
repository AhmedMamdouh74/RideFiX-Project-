# Activity Report Service

This service provides comprehensive activity tracking and reporting for the RideFix system. It tracks various types of activities across different entities and provides filtered reports.

## Features

- **Multi-Entity Activity Tracking**: Tracks activities from Emergency Requests, Car Maintenance Records, Technician/Car Owner registrations, Reviews, and Chat Sessions
- **Time-Based Filtering**: Filter activities by time range (default: last 12 hours, max: 7 days)
- **Activity Type Filtering**: Filter by activity type (Created, Completed, Updated, Registered)
- **Entity Type Filtering**: Filter by entity type (EmergencyRequest, CarMaintenanceRecord, Technician, CarOwner, Review, ChatSession)
- **Arabic Time Formatting**: Provides human-readable time differences in Arabic

## API Endpoints

### 1. Get Recent Activities
```
GET /api/ActivityReport/recent?hoursBack=12
```
Returns the most recent activities across all entity types.

**Parameters:**
- `hoursBack` (optional): Number of hours to look back (1-168, default: 12)

### 2. Get Activities by Type
```
GET /api/ActivityReport/by-type/{activityType}?hoursBack=12
```
Returns activities filtered by activity type.

**Parameters:**
- `activityType`: Type of activity (Created, Completed, Updated, Registered)
- `hoursBack` (optional): Number of hours to look back (1-168, default: 12)

### 3. Get Activities by Entity Type
```
GET /api/ActivityReport/by-entity/{entityType}?hoursBack=12
```
Returns activities filtered by entity type.

**Parameters:**
- `entityType`: Type of entity (EmergencyRequest, CarMaintenanceRecord, Technician, CarOwner, Review, ChatSession)
- `hoursBack` (optional): Number of hours to look back (1-168, default: 12)

## Activity Types Tracked

### Emergency Requests
- **Created**: New emergency service requests
- **Completed**: Completed emergency requests
- **Updated**: Status updates on emergency requests

### Car Maintenance Records
- **Completed**: Completed maintenance records

### User Registrations
- **Registered**: New technician and car owner registrations

### Reviews
- **Created**: New reviews added to the system

### Chat Sessions
- **Created**: New chat sessions initiated

## Response Format

```json
{
  "success": true,
  "message": "تم جلب النشاطات الأخيرة بنجاح",
  "data": {
    "activities": [
      {
        "activityType": "Created",
        "description": "تم إنشاء طلب خدمة طوارئ جديد",
        "timestamp": "2024-01-15T10:30:00Z",
        "timeAgo": "منذ 5 دقائق",
        "entityType": "EmergencyRequest",
        "entityId": 123
      }
    ],
    "totalCount": 1,
    "reportGeneratedAt": "2024-01-15T10:35:00Z"
  }
}
```

## Implementation Details

### Service Architecture
- **IActivityReportService**: Service abstraction interface
- **ActivityReportService**: Main service implementation using LINQ expressions
- **ActivityReportController**: API controller with authorization
- **TimeHelper**: Utility class for Arabic time formatting

### Database Queries
The service uses LINQ expressions to query multiple entities:
- EmergencyRequests (TimeStamp, EndTimeStamp)
- CarMaintenanceRecords (PerformedAt)
- Technicians (ApplicationUser.CreatedAt)
- CarOwners (ApplicationUser.CreatedAt)
- Reviews (DateTime)
- ChatSessions (StartAt)

### Performance Considerations
- Each entity type is queried separately with Take(12) limit
- Results are combined and ordered by timestamp
- Final result is limited to top 12 most recent activities
- Uses async/await for database operations

## Usage Examples

### Get all recent activities
```csharp
var activities = await _serviceManager.activityReportService.GetLastActivitiesAsync(24);
```

### Get only completed activities
```csharp
var completedActivities = await _serviceManager.activityReportService.GetActivitiesByTypeAsync("Completed", 48);
```

### Get only emergency request activities
```csharp
var emergencyActivities = await _serviceManager.activityReportService.GetActivitiesByEntityTypeAsync("EmergencyRequest", 6);
```

## Security
- All endpoints require authorization (`[Authorize]` attribute)
- Input validation for time ranges (1-168 hours)
- Proper error handling and logging

## Future Enhancements
- Add pagination support for large datasets
- Implement activity caching for better performance
- Add more granular activity types
- Support for custom date ranges
- Activity export functionality

